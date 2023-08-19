import './App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Login from './components/Login';
import Signup from './components/Signup';
import Layout from './components/Layout';

function App() {
  return (
    <BrowserRouter>
      <Routes>
      <Route path='/' element={<Layout/>} />
        
      </Routes>
    </BrowserRouter>
  )
}

export default App;