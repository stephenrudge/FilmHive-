import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { CommentBox } from "./Comment";
import { Watchlist } from "./Watchlist";
import "./MovieDetails.css"
import { CommentComponent } from "./CommentCard";
import { StarRating } from "./StarRating/StarRating";



export const MovieDetails = () => {
    var appUser = localStorage.getItem("capstone_user");
    var appUserObject = JSON.parse(appUser);
    
    const { movieId } = useParams();
    const [currentMovie, setCurrentMovie] = useState([]);
  
  


    useEffect(() => {

        fetchMovie()
    }, []
    )


    const userId = localStorage.getItem("capstone_user")
    const fetchMovie = async () => {
        try {
            const response = await fetch(`https://localhost:7211/api/Movie/${movieId}?userId=${userId}`);

            const data = await response.json();
            setCurrentMovie(data);


        } catch (error) {
            console.error('Error fetching search results:', error);
        }
    };


  

   

    const handleSubmit = async (comment) => {
        const commentBuilder = {

            "movieId": movieId,
            "userId": userId,
            "text": comment,
        }
   
        try {
            const response = await fetch('https://localhost:7211/api/Comment', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(commentBuilder),
            });

            if (response.ok) {
                console.log('Comment submitted successfully');
                fetchMovie()

            } else {
                console.error('Failed to submit comment');
            }
        } catch (error) {
            console.error('Error submitting comment:', error);
        }
    };

    

       

    return (
        currentMovie &&
        <div className="MovieForm">
            {currentMovie.map(movie => {
                return (
                    <>
                        <div key={(movie.movieId)} className="title">

                            <b><u>{movie.title}</u></b>
                        </div>
                        <div className="MovieDetails">
                            <img src={`https://image.tmdb.org/t/p/w200${movie.posterPath}`} key={movie.id} />
                           
                           <StarRating />
                           
                            <div className="comment" >
                                <h3>Comments</h3>
                                {movie.comments.map(comment => {
                                    return (
                                        <>

                                        < CommentComponent 
                                        comment= {comment} 
                                        onUpdate= {fetchMovie}
                                   
                                        key= {comment.id}/>

                                        </> 

                                        )
                                }
                                )
                                }
                                {/* if no comments dont show button */}
                                    <h4>Add New Comment</h4>
                                <  CommentBox handleSubmit={handleSubmit} />
                            </div>

                        </div>
                    </>
                )
            })}
        </div>
    )
}