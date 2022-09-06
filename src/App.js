import Login from './components/Login';
import {Routes, Route} from "react-router-dom"
import { Home } from './components/Home';
import { getAuth, onAuthStateChanged } from 'firebase/auth';
import { app } from './firebase/firebase';
import { useState } from 'react';
import { Coordenadas } from './components/Coordenadas';
import { Register } from './components/Register';

const auth = getAuth(app)
function App() {


  const [usuario, setUsuario] = useState(null);
  onAuthStateChanged(auth, (user) => {
    if (user) {
      setUsuario(user)
    }
    else{
      setUsuario(null)
    }
  });

  return (

      <Routes>
        <Route path='/' element={<Login/>}/>
        <Route path='/home' element={<Home/>}/> 
        <Route path='/login' element={<Login/>}/>
        <Route path='/register' element={<Register/>}/>
      </Routes>
    
  );
}

export default App;
