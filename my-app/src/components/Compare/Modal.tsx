import {
  Button,
  Grid,
  IconButton,
  Modal,
  Paper,
  makeStyles,
} from '@material-ui/core';
import Icon from '@material-ui/icons/AssistantPhoto';

import React, { useState } from 'react';
import Tree from 'react-d3-tree';

interface Props {
  className: string;
  ast: any;
}

const useStyles = makeStyles((theme) => ({
  modal: {
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
  },
  modalContent: {
    backgroundColor: 'white',
    outline: 'none',
    padding: theme.spacing(3),
    height: '90vh', // Set the height to 90% of the viewport height
    width: '90%', // Set the width to 90% of the viewport width
    overflowY: 'auto', // Enable vertical scrolling if the content exceeds the height
    borderRadius: theme.shape.borderRadius,
    position: 'relative',
  },
  closeButton: {
    position: 'absolute',
    top: theme.spacing(1),
    right: theme.spacing(1),
  },
}));

const MyModal = (props: Props) => {
  const classes = useStyles();

  const [isOpen, setIsOpen] = useState(false);

  const openModal = () => setIsOpen(true);
  const closeModal = () => setIsOpen(false);

  const transformJsonNew = (json: any) => {
    if (!json) return undefined;
    const result = {
      name: json?.text || '',
      children: [],
    };

    if (json.children) {
      json.children.forEach((child: any) => {
        if (!child) return;
        const childEl: any = result.children;
        childEl.push(transformJsonNew(child));
      });
    }

    return result;
  };

  return (
    <>
      <IconButton onClick={openModal} aria-label="Delete">
        <Icon />
      </IconButton>
      <Modal open={isOpen} onClose={closeModal} classes={classes.modal}>
        <>
          <Paper className={props.className} style={{ padding: '20px' }}>
            <div style={{minWidth:'500px', height:'600px'}}>
              <Tree
                data={transformJsonNew(props.ast)}
                orientation="vertical"
                rootNodeClassName="node__root"
                branchNodeClassName="node__branch"
                leafNodeClassName="node__leaf"
                translate={{ x: 300, y: 20 }}
                nodeSize={{ x: 250, y: 150 }}
                zoom={0.4}
                separation={{ siblings: 1, nonSiblings: 2 }}
              />
            </div>
          </Paper>
          <Button onClick={closeModal} className={classes.closeButton}>
            Close
          </Button>
        </>
      </Modal>
    </>
  );
};

export default MyModal;
