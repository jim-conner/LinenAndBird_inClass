import { useEffect, useState } from 'react';
import './App.css';
import BirdList from './components/BirdList';
import { getAllBirds } from './helpers/data/birdData';

function App() {
  // let birds = [
  //   {name:"steve"},
  //   {name:"john"},
  //   {name:"teddy"}
  // ];
// store token auth for later ... add that part here too from PR
const [birds, setBirds] = useState([]);

useEffect(() => {
  getAllBirds().then(setBirds())
}, []);

  return (
    <div className="App">
      {/* {you'll want to add the buttons here for auth} */}
      <BirdList birds={birds}/>
    </div>
  );
}

export default App;
