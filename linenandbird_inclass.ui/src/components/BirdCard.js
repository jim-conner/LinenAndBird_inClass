import React from 'react'

export default function BirdCard({bird}) {
  //or use props let bird = {...props}
  
  return (
    <div>
      <h4>{bird.name}</h4> <br/>
      Type: {bird.type} <br/>
      Color: {bird.color} <br/>
    </div>
  )
}
