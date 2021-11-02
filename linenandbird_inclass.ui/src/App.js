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

const [birds, setBirds] = useState([]);

useEffect(() => {
  getAllBirds().then(data => setBirds(data))
}, []);

  return (
    <div className="App">
      <BirdList birds={birds}/>
    </div>
  );
}

export default App;
