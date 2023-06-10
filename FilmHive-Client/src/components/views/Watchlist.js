import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import "./Watchlist.css"


var appUser = localStorage.getItem("capstone_user");
appUser = parseInt(appUser) 

export const Watchlist = () => {
    
    const navigate = useNavigate();

    const [userMovies, setUserMovies] = useState()


    useEffect(() => {
        // Function to call the API endpoint with the search query
       
    
        const fetchSearchResults = async () => {
            try {
                const userId = localStorage.getItem("capstone_user")
                const response = await fetch(`https://localhost:7211/api/Movie/watchlist/${userId}`);
             
                const data = await response.json();
                setUserMovies(data);
                console.log(userMovies)
              
                
            } catch (error) {
                console.error('Error fetching search results:', error);
            }
        };
        
        if (userMovies === undefined) {
                
                fetchSearchResults();
            } 
            
        }
     ,[]);

    const handleInputChange = (event) => {
        setUserMovies(event.target.value);
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        listofMovies(userMovies);
    };

 
      
      const handleClickDeleteMovie = async (id) => {
        await fetch(`https://localhost:7211/api/UserMovie/${id}`, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
            },
            bbody: JSON.stringify(), 
                headers: {
                    "Content-Type": "application/json"
                }
            })
          await fetchWatchlist()
        }

        const fetchWatchlist = async () => {
            try {
                const userId = localStorage.getItem("capstone_user")
                const response = await fetch(`https://localhost:7211/api/Movie/watchlist/${userId}`);
             
                const data = await response.json();
                setUserMovies(data);
                console.log(userMovies)
              
                
            } catch (error) {
                console.error('Error fetching search results:', error);
            }
        };
        
        useEffect(() => {
            // Fufeytnction to call the API endpoint with the search query
          
        
            if (userMovies === undefined) {
                    
                 fetchWatchlist()
                } 
                
            }
         ,[]);



    return ( 
            userMovies && userMovies.length > 0 ? 
           <div> <h3>Your Watchlist</h3>
            <div className="userMovies">
                
            {userMovies && userMovies.map( (movie) => {
                return (
               <div key={movie.userMovieId}>
                <img src={`https://image.tmdb.org/t/p/w200${movie.posterPath}`} 
                  onClick={() => {
                      navigate(`/MovieDetails/${movie.id}`);
                    }} /> 
                    
                    <b>{movie.title}</b> 
                  <br></br>
                  <br></br>
                  <br></br>
                
                  <button onClick={() => {handleClickDeleteMovie(movie.userMovieId)}} className="deleteButton"> Remove Movie</button>

        
                  
                  
                   {/* <div className="comment"> {CommentBox} </div> */}
                </div>
               )
            } 
            )
            
            }
            </div>
        </div>
        :
        <div>
            <p>You have no movies <a href="/discover">click here to disover new movies</a>  </p>
        </div>
    )
}
            