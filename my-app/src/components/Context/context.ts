import { createContext } from 'react';

export type MyData = {
  files: File[];
  selectedFiles: File[];
};

export type Context = {
  myData: MyData;
  selectFiles: (files: any) => void;
  updateFiles: (files: any) => void;
};

export const defaultValue: any = {
  myData: {
    files: [],
    selectedFiles: [],
  },
};

export const MyContext = createContext<Context>(defaultValue);
