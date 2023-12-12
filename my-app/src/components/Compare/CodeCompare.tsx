import React, { useState, useEffect, useContext } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';
import { awsAPI, detectionAPI } from '../../functions/api';
import CompareFiles from './CompareFiles';
import { Container, Typography } from '@material-ui/core';
import Tree from 'react-d3-tree';
import '../../index.css';
import { Divider } from '@material-ui/core';
import { MyContext } from '../Context/context';
import { CloudUpload } from '@material-ui/icons';
import MyModal from './Modal';
import HistogramChart from './HistogramChart';

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  button: {
    padding: theme.spacing(2),
  },
  paper: {
    padding: theme.spacing(2),
    textAlign: 'left',
    color: theme.palette.text.secondary,
    minHeight: '700px',
    overflowY: 'auto',
  },
  code: {
    padding: theme.spacing(1),
    fontSize: '14px',
    fontFamily: 'monospace',
    whiteSpace: 'pre-wrap',
    wordWrap: 'break-word',
  },
  codeWrapper: {
    display: 'flex',
    flexDirection: 'row',
    alignItems: 'flex-start',
    justifyContent: 'space-between',
  },
  added: {
    backgroundColor: theme.palette.success.dark,
    color: theme.palette.text.primary,
  },
  removed: {
    backgroundColor: theme.palette.error.dark,
    color: theme.palette.text.primary,
  },
  label: {
    fontSize: '16px',
    fontWeight: 'bold',
    marginBottom: '10px',
  },
  resultContainer: {
    marginTop: '50px',
  },
  uploadButton: {
    marginLeft: '10px',
  },
  analyzeButton: {
    marginRight: '10px',
  },
}));

interface Props {
  selectedItems: File[];
  items: File[];
}

const castToMessage = (result: any) => {

  return `Similarity Score ${result}`;
};

const transformJson = (json: any) => {
  if (!json) return undefined;
  const result = {
    name: json.metadata?.['title'] || json.type,
    children: [],
  };

  if (json.children) {
    json.children.forEach((child: any) => {
      const childEl: any = result.children;
      childEl.push(transformJson(child));
    });
  }

  return result;
};


export const CodeCompare = (props: Props) => {
  const { updateFiles } = useContext(MyContext);
  const classes = useStyles();
  const [code1, setCode1] = useState('');
  const [code2, setCode2] = useState('');
  const [result, setResult] = useState<any>(null);
  const [showResult, setShowResult] = useState<boolean>(false);

  const [tree, setTree] = useState<any>(null);

  useEffect(() => {
    if (props.selectedItems.length === 0) return;
    awsAPI
      .post(
        'filter',
        props.selectedItems.map((x) => x.name)
      )
      .then((files: any) => {
        setCode1(files[0]);
        setCode2(files[1] || ' ');
      });
  }, [props.selectedItems]);

  const analyze = () => {
    const tokenize = detectionAPI.post('tokenize', {
      fileNames: props.items.map(x => x.name),
    });

    Promise.all([tokenize])
      .then(([jsonTree]) => {
        const result = jsonTree as any;
        setTree(jsonTree);
        setShowResult(true);
      })
      .catch((error: any) => console.log(error));
  };

  const handleUpload = async (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.files) {
      const fileList = Array.from(event.target.files);
      updateFiles(fileList);

      const formData = new FormData();
      for (let i = 0; i < fileList.length; i++) {
        formData.append('files', fileList[i]);
      }

      awsAPI
        .post('/list', formData, {
          headers: {
            'Content-Type': 'multipart/form-data',
          },
        })
        .then((response) => console.log(response))
        .catch((error) => console.log(error));

      detectionAPI.get('get-results').then((response)=>{
        setResult(response)
      });
    }
  };

  return (
    <div className={classes.codeWrapper}>
      <Grid container spacing={3}>
        <Grid item xs={12}>
          <Paper className={classes.paper}>
            <CompareFiles left={code1} right={code2} className={classes.paper} ast1={tree?.ast1} ast2={tree?.ast2} />
          </Paper>
        </Grid>
        <Divider />
        {/* {result && (
          <Container maxWidth="sm" className={classes.resultContainer}>
            <Typography variant="h4" component="h1" align="center">
              {castToMessage(result)}
            </Typography>
            <Typography variant="body1" align="center"></Typography>
          </Container>
        )} */}
        <Grid container justify="space-between" alignItems="center">
          <Grid item>
            <Button
              variant="contained"
              color="primary"
              component="label"
              className={classes.uploadButton}
              startIcon={<CloudUpload />}
            >
              Upload Files
              <input type="file" hidden multiple onChange={handleUpload} />
            </Button>
          </Grid>
          <Grid item>
            <Button
              className={classes.analyzeButton}
              variant="contained"
              color="primary"
              onClick={analyze}
            >
              Analyze
            </Button>
          </Grid>
        </Grid>
        {showResult && tree && result && <HistogramChart data={result} simarity={tree?.similarity} items={props.items}/>}
      </Grid>
    </div>
  );
};
