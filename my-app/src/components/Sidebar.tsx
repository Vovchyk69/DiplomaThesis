import React, { useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { CloudUpload } from '@material-ui/icons';
import { Button } from '@material-ui/core';
import { FileList } from './FileList';

const useStyles = makeStyles((theme) => ({
  root: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    padding: theme.spacing(2),
  },
  uploadButton: {
    marginBottom: theme.spacing(2),
  },
}));

interface Props {
  selectedFiles: File[];
  handleFileSelect: (event: any, file: File) => void;
}

const VerticalUploadTree = (props: Props) => {
  const classes = useStyles();
  const [files, setFiles] = useState<File[]>([]);

  const handleUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.files) {
      const fileList = Array.from(event.target.files);
      setFiles(fileList);
    }
  };

  return (
    <div className={classes.root}>
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
      <FileList files={files} selectedFiles={props.selectedFiles} onSelect={props.handleFileSelect} />
    </div>
  );
};

export default VerticalUploadTree;
