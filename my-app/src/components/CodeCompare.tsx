import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';
import { awsAPI, detectionAPI } from '../functions/api';
import { diff_match_patch } from 'diff-match-patch';
import CompareFiles from './CompareFiles';
import { Container, Typography } from '@material-ui/core';

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
  resultContainer: {
    marginTop: '50px',
  },
}));

interface Props {
  items: File[];
}

const castToMessage = (result: any) => {
  const percentage = result.documents[0].average_generated_prob;
  if (percentage >= 0.95 && percentage <= 1)
    return 'Your text is likely to be written entirely by AI';

  if (percentage >= 0.65 && percentage <= 0.95)
    return 'Your text may include parts written by AI';

  return 'Your text is likely to be written entirely by Human';
};

export const CodeCompare = (props: Props) => {
  const classes = useStyles();
  const [code1, setCode1] = useState('');
  const [code2, setCode2] = useState('');
  const [result, setResult] = useState<any>(null);

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

  const analyze = () => {
    detectionAPI
      .post('gpt-zero', {
        document: props.items[0].name,
      })
      .then((result) => setResult(result));
  };

  return (
    <div className={classes.codeWrapper}>
      <Grid container spacing={3}>
        <Grid item xs={12}>
          <Paper className={classes.paper}>
            <CompareFiles left={code1} right={code2} />
          </Paper>
        </Grid>

        <Grid container justify="space-between" alignItems="center">
          <Grid item>
            <Button variant="contained" color="primary" onClick={analyze}>
              Compare
            </Button>
          </Grid>
          <Grid item>
            <Button variant="contained" color="primary" onClick={analyze}>
              Analyze
            </Button>
          </Grid>
        </Grid>
        {result && (
          <Container maxWidth="sm" className={classes.resultContainer}>
            <Typography variant="h4" component="h1" align="center">
              {castToMessage(result)}
            </Typography>
            <Typography variant="body1" align="center">
            </Typography>
          </Container>
        )}
      </Grid>
    </div>
  );
};
