import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';
import { awsAPI, detectionAPI } from '../functions/api';
import { diff_match_patch } from 'diff-match-patch';
import CompareFiles from './CompareFiles';

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  button: {
    marginRight: 'auto',
  },
  paper: {
    padding: theme.spacing(2),
    textAlign: 'left',
    color: theme.palette.text.secondary,
    minHeight: '300px',
    maxHeight: '500px',
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
}));

interface Props {
  items: File[];
}

const dmp = new diff_match_patch();

export const CodeCompare = (props: Props) => {
  const classes = useStyles();
  const [code1, setCode1] = useState('');
  const [code2, setCode2] = useState('');
  const [diff, setDiff] = useState<any>([]);

  useEffect(() => {
    if (props.items.length === 0) return;
    awsAPI
      .post(
        'filter',
        props.items.map((x) => x.name)
      )
      .then((files: any) => {
        setCode1(files[0]);
        setCode2(files[1] || ' ');
      });
  }, [props.items]);

  const handleCompare = () => {
    detectionAPI.post('gpt-zero', {
      document: props.items[0].name,
    });

    const diffText = dmp.diff_main(code1, code2);
    dmp.diff_cleanupSemantic(diffText);
    setDiff(diffText);
  };

  return (
    <div className={classes.codeWrapper}>
      <Grid container spacing={3}>
        <Grid item xs={12}>
          <Paper className={classes.paper}>
            <CompareFiles left={code1} right={code2} />
          </Paper>
        </Grid>
        <Grid item xs={12}>
          <Button className={classes.button} variant="contained" color="primary" onClick={handleCompare}>
            Compare
          </Button>
        </Grid>
      </Grid>
    </div>
  );
};
