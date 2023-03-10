import React, { useContext } from 'react';
import { Checkbox, List, ListItem, ListItemText } from '@material-ui/core';
import { MyContext } from './Context/context';

interface Props {
  files: File[];
  selectedFiles: File[];
}

export const FileList = (props: Props) => {
  const { files, selectedFiles } = props;
  const { selectFiles } = useContext(MyContext);

  const handleFileSelect = (event: any, file: File) => {
    const checkedFiles: File[] =
      event.target.checked && selectedFiles.length === 2
        ? selectedFiles.slice(1)
        : selectedFiles;

    event.target.checked
      ? selectFiles([...checkedFiles, file])
      : selectFiles(checkedFiles.filter((f) => f.name !== file.name));
  };

  return (
    <List>
      {files.map((file, index) => (
        <ListItem key={index} onClick={(e) => handleFileSelect(e, file)} button>
          <Checkbox checked={selectedFiles?.includes(file)} />
          <ListItemText primary={file.name} />
        </ListItem>
      ))}
    </List>
  );
};
