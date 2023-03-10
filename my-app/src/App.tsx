import React from 'react';
import { ThemeProvider, createTheme } from '@material-ui/core/styles';
import { MainPage } from './components/MainPage';
import MyContextProvider from './components/Context/ContextProvider';

const theme = createTheme({
  palette: {
    primary: {
      main: '#F1F1F1',
    },
    secondary: {
      main: '#FFB300',
    },
    text: {
      primary: '#333333',
      secondary: '#7D7D7D',
    },
    background: {
      default: '#FFFFFF',
    },
  },
  typography: {
    fontFamily: ['Roboto', 'sans-serif'].join(','),
    fontWeightRegular: 400,
    fontWeightMedium: 500,
    fontWeightBold: 700,
    h1: {
      fontSize: '3.5rem',
      fontWeight: 'bold',
    },
    h2: {
      fontSize: '2.5rem',
      fontWeight: 'bold',
    },
    h3: {
      fontSize: '2rem',
      fontWeight: 'bold',
    },
    h4: {
      fontSize: '1.5rem',
      fontWeight: 'bold',
    },
    h5: {
      fontSize: '1.25rem',
      fontWeight: 'bold',
    },
    h6: {
      fontSize: '1rem',
      fontWeight: 'bold',
    },
  },
  overrides: {
    MuiButton: {
      root: {
        borderRadius: 8,
        textTransform: 'none',
        padding: '10px 20px',
      },
    },
  },
});

const App: React.FC = () => {
  return (
    <ThemeProvider theme={theme}>
      <MyContextProvider>
        <MainPage />
      </MyContextProvider>
    </ThemeProvider>
  );
};

export default App;
