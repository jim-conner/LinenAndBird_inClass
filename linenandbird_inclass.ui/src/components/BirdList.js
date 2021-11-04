import React from "react";
import BirdCard from './BirdCard'

export default function BirdList({birds}) {

  let birdCards = birds?.map(bird => (
    <BirdCard 
      key={bird.name} // will need a diff key irl
      bird={bird}
    >
    </BirdCard>))
  

  return (
  <div style={{display: "flex"}}>
    {birdCards}
    {/* <BirdCard bird={ {name:"steve", type:"bluejay", color:"purple"} }/> */}
  </div>
  )
}
