
import { useState, useEffect } from 'react';
import { Navigate, useParams } from 'react-router-dom';
import { Comment } from './CommentCard';
import "./Comment.css"

export const CommentBox = ({handleSubmit}) => {
  const [comment, setComment] = useState("");

  let userId = localStorage.getItem("capstone_user");
  userId = parseInt(userId)

  const { movieId } = useParams()
  
const handleClick = () => {
const commentToSave = comment
setComment('')
handleSubmit(commentToSave)

} 



  const UpdateComment = () => {
    fetch(`https://localhost:7211/api/Comment/${id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(comment),
    })
  };

  // const DeleteComment = () => {
  //   fetch(`https://localhost:7211/api/Comment/{id}`, {
  //     method: "DELETE",
  //     headers: {
  //       "Content-Type": "application/json",
  //     },
  //     body: JSON.stringify(movie),
  //   })
  // };


return (
  <>
  <div>
{/* < Comment 
key ={`Comment --${Comment.id}`}
text={Comment.text}
movieId={Comment.movieId}
userId={Comment.userId} /> */}
<textarea
placeholder="Enter your comment..."
onChange={(event) => setComment(event.target.value)}>

</textarea>
      <button onClick={handleClick} className="submitButton">Submit Comment</button>
    
</div>
  
  </>
);


  };
