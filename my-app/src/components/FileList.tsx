import React from 'react';
import { Checkbox, List, ListItem, ListItemText } from '@material-ui/core';

interface Props {
  files: File[];
  selectedFiles: File[];
  onSelect: (event: any, file: File) => void;
}

export const FileList = (props: Props) => {
  const { files, selectedFiles, onSelect } = props;

  return (
    <List>
      {files.map((file, index) => (
        <ListItem key={index} onClick={(e) => onSelect(e, file)} button>
          <Checkbox checked={selectedFiles?.includes(file)} />
          <ListItemText primary={file.name} />
        </ListItem>
      ))}
    </List>
  );
};
