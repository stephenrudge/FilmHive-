import { useEffect, useState } from "react";
import "./CommentCard.css"

export const CommentComponent = ({ comment, onUpdate }) => {

    const [formComment, setFormComment] = useState("")

    const [displayUpdateForm, setDisplayUpdateForm] = useState(false)

    useEffect(() => {
        setFormComment(comment.text)
    }, [comment]
    )


    
        // Your component logic here
        
        
        const deleteComment = async () => {
            try {
                const response = await fetch(
                    `https://localhost:7211/api/Comment/${comment.id}`,
                    {
                        method: "DELETE",
                        headers: {
                            "Content-Type": "application/json",
                        },
                        body: JSON.stringify({
                            id: comment.id,
                            userId: comment.userId,
                            movieId: comment.movieId,
                            text: comment.text,
                        }),
                    }
                );

                if (response.ok) {
                    console.log("Comment deleted successfully!");
                    onUpdate()
                } else {
                    console.error("Failed to delete comment");
                }
            } catch (error) {
                console.error("An error occurred while deleting the comment:", error);
                
            };}
        
            
 







const UpdateComments = () => {

    fetch(`https://localhost:7211/api/Comment/${comment.id}`, {
        method: "PUT",
        body: JSON.stringify(
            {
                id:comment.id,
                userId: comment.userId,
                movieId: comment.movieId,
                text: formComment
            }),
        headers: {
            "Content-Type": "application/json"
        }
    })
        .then(() => {
            setDisplayUpdateForm(false)
            onUpdate()
        }
        )
}




return (
    <>

        {displayUpdateForm
            ?
            <> <input
                required
                autoFocus
                type="text"
                className="form-control"
                placeholder="Comment"
                value={formComment}
                onChange={(evt) => {

                    setFormComment(evt.target.value);

                }}
            />
                <div onClick={UpdateComments} className="saveButton">
                    <div className="box">S</div>
                    <div className="box">A</div>
                    <div className="box">V</div>
                    <div className="box">E</div>
                    <div className="box"> </div>
                    <div className="box"> </div>
                    <div className="box"> </div>
                </div>
            </>
            : <> </>

        }


        <p key={comment.id}>{comment.text}</p>



        <button onClick={() => {
            setDisplayUpdateForm(true)
        }} className="UpdateCommentButton">Update Comment</button>

        <button onClick={deleteComment} className="noselect"><span className="text">Delete Comment</span><span className="icon"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24"><path d="M24 20.188l-8.315-8.209 8.2-8.282-3.697-3.697-8.212 8.318-8.31-8.203-3.666 3.666 8.321 8.24-8.206 8.313 3.666 3.666 8.237-8.318 8.285 8.203z"></path></svg></span></button>


    </>
)

    }