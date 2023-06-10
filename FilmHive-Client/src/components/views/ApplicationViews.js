
import { Route, Routes, Outlet } from "react-router-dom";
import { Login } from "../auth/Login";
import { MovieSearchBox } from "./MovieSearch";
import "./ApplicationViews.css"
import { Watchlist } from "./Watchlist";
import { MovieDetails } from "./MovieDetails";
import { Home } from "./Home";



export const ApplicationViews = () => {
  const localMovieUser = localStorage.getItem("capstone_user");
  const bookUserObject = JSON.parse(localMovieUser);

  if (bookUserObject) {
    return (
      <Routes>
        <Route path="/" element={
          <>


            <Outlet />
          </>
        }>



          <Route path="/login" element={<Login />} />
          <Route path="/Discover" element={<MovieSearchBox />} />
          <Route path="/Watchlist" element={<Watchlist />} />
          <Route path="/MovieDetails/:movieId" element={<MovieDetails />} />
        
        
        


        </Route>
      </Routes>
    )

  }
}