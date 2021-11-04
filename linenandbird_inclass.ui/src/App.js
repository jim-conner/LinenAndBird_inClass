import { useEffect, useState } from 'react';
import './App.css';
import BirdList from './components/BirdList';
import { signInUser } from './helpers/auth';
import getAllBirds from './helpers/data/birdData';
import firebase from 'firebase/compat/app';
import 'firebase/compat/auth';
import 'firebase/compat/firestore';

function App() {
  // let birds = [
  //   {name:"steve"},
  //   {name:"john"},
  //   {name:"teddy"}
  // ];
// store token auth for later ... add that part here too from PR
const [birds, setBirds] = useState([]);
const [user, setUser] = useState({});

useEffect(() => getAllBirds().then(setBirds), []);
useEffect(() => {
  firebase.auth().onAuthStateChanged((user) => {
    if (user) {             
      
      //store the token for later   
      user.getIdToken().then((token) => sessionStorage.setItem("token", token));
      
      setUser(user);
    } else {
      setUser(false);
    }
  });
}, []); 
  return (
    <div className="App">
      <div className="d-flex justify-content-center">
        <button className="signin-button google-logo" onClick={signInUser}>
          <i className="fas fa-sign-out-alt"></i> Sign In
        </button>
      </div>
      
      <BirdList birds={birds}/>
    </div>
  );
}

export default App;
