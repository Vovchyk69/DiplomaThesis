import React, { useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Paper from '@material-ui/core/Paper';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  paper: {
    padding: theme.spacing(2),
    textAlign: 'center',
    color: theme.palette.text.secondary,
  },
}));

interface Props{
  items: File[];
}

export const CodeCompare = (props: Props) => {
  const classes = useStyles();
  const [code1, setCode1] = useState('');
  const [code2, setCode2] = useState('');

  const handleCompare = () => {
    // Add logic to compare the two code snippets
    console.log('Comparing code1:', code1, 'and code2:', code2);
  };

  // const reader = new FileReader();
  // reader.readAsText(props.items[0]);
  // reader.onload = (event) => {
  //   setCode1(event.target?.result as unknown as string);
  // };

  return (
    <div className={classes.root}>
      <Grid container spacing={3}>
        <Grid item xs={6}>
          <Paper className={classes.paper}>
            <TextField
              id="code1"
              label="Code 1"
              multiline={true}
              rows={20}
              fullWidth
              variant="outlined"
              value={code1}
              onChange={(event) => setCode1(event.target.value)}
            />
          </Paper>
        </Grid>
        <Grid item xs={6}>
          <Paper className={classes.paper}>
            <TextField
              id="code2"
              label="Code 2"
              multiline={true}
              rows={20}
              fullWidth
              variant="outlined"
              value={code2}
              onChange={(event) => setCode2(event.target.value)}
            />
          </Paper>
        </Grid>
        <Grid item xs={12}>
          <Button variant="contained" color="primary" onClick={handleCompare}>
            Compare
          </Button>
        </Grid>
      </Grid>
    </div>
  );
}