import React, { useState } from 'react';
import { defaultValue, MyContext, MyData } from './context';

interface Props {
  children: any;
}

const MyContextProvider = (props: Props) => {
  const [myData, setMyData] = useState<MyData>(defaultValue);

  const updateFiles = (files: any) => {
    setMyData({ ...myData, files });
  };

  const selectFiles = (selectedFiles: any) => {
    setMyData({ ...myData, selectedFiles });
  };

  return (
    <MyContext.Provider value={{ myData, updateFiles, selectFiles }}>
      {props.children}
    </MyContext.Provider>
  );
};

export default MyContextProvider;
