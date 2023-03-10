import React, { useContext } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { FileList } from './FileList';
import { MyContext } from './Context/context';

const useStyles = makeStyles((theme) => ({
  root: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    width: '180px',
    padding: theme.spacing(2),
  },
  uploadButton: {
    marginBottom: theme.spacing(2),
  },
}));

interface Props {}

const Sidebar = (props: Props) => {
  const classes = useStyles();
  const { myData } = useContext(MyContext);

  return (
    <div className={classes.root}>
      <FileList files={myData.files || []} selectedFiles={myData.selectedFiles || []} />
    </div>
  );
};

export default Sidebar;
