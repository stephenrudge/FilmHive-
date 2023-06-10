import React, { useState, useEffect } from 'react';
import "./MovieSearch.css"
import { useNavigate} from 'react-router-dom';


export const MovieSearchBox = ({ onSearch }) => {
  const [searchQuery, setSearchQuery] = useState('');
  const [searchResults, setSearchResults] = useState([]);


  var appUser = localStorage.getItem("capstone_user");
  appUser = parseInt(appUser)

  useEffect(() => {
    // Function to call the API endpoint with the search query
    const fetchSearchResults = async () => {
      try {
        const response = await fetch(`https://localhost:7211/search?title=${searchQuery}`);
        const data = await response.json();
        setSearchResults(data);
      } catch (error) {
        console.error('Error fetching search results:', error);
      }
    };
    console.log(searchResults)

    if (searchQuery) {
      fetchSearchResults();
    } else {
      setSearchResults([]);
    }
  }, [searchQuery]);

  // const UsersInfo = () => {
  //   const users = {
  //     name: users.name,
  //     email: users.email,
  //     password: users.password,

  // }
  //   fetch(`https://localhost:7211/api/Users`, {
  //     method: "GET",
  //     headers: {
  //       "Content-Type": "application/json",
  //     },
  //     body: JSON.stringify(appUser),
  //   })
  // };

  const url2 = "https://localhost:7211/api/Movie"

  const displayMovies = async () => {
    const fetchData = await fetch(`${url2}`)
    const fetchJson = await fetchData.json()
    setMovies(fetchJson)
  }
  const [movies, setMovies] = useState([])
  const [updateDom, setUpdateDom] = useState(false)

  useEffect(
    () => {
      displayMovies()
    }, [updateDom]
  )

  const handleInputChange = (event) => {
    setSearchQuery(event.target.value);
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    onSearch(searchQuery);
  };
 
 

  
  
  const handleClickAddMovie = (event) => {
    console.log(event)
    const taco = {
      id: 0,
      movieId: parseInt(event.target.id),
      userId: appUser,
   
    }
    
  fetch(`https://localhost:7211/api/UserMovie`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(taco),
  })
};



return (
  <div>
    <div>
      {/* <h1> Hello {appUser.name}</h1> */}
      <h2>What movie do you want to watch?</h2>

    </div>
    {/* <form onSubmit={handleSubmit}>
      <input
        type="text"
        value={searchQuery}
        onChange={handleInputChange}
        placeholder="Enter the title of movie"
      />
     
    </form> */}

<div className="inputbox">
    <input required="required" type="text" value={searchQuery} onChange={handleInputChange} placeholder='Enter Movie Title' />
    
    <i></i>
</div>




    {/* Render the search results */}
    {searchResults.length > 0 ? (
      <ul className="test">
        {searchResults.map((result) => (
          <li key={result.id}>
            <b><u>{result.title}</u></b>

            <ul>
              <img
                src={`https://image.tmdb.org/t/p/w200${result.posterPath}`  } id = {result.id}
                onClick={(event) => {
                  if (window.confirm("Would you like to add this to your watchlist?")) {
                    handleClickAddMovie(event)
                    console.log("Added to watchlist:", result.title);
                  } else {
                    // User clicked "No"
                    console.log("Did not add to watchlist:", result.title);
                  }

                }}
              />

            </ul>
            <i>{result.overview}</i>
          </li>
        )
        )}
      </ul>
    ) : null}


  </div>

)

}
