
import { Route, Routes } from "react-router-dom";
import { Authorized } from "./views/Authorized";
import { ApplicationViews } from "./views/ApplicationViews";
import { Login } from "./auth/Login";
import { Register } from "./auth/Register";
import { NavBar } from "./views/NavBar";
import { Home } from "./views/Home";


export const FilmHive = () => {
  return (
    <Routes>
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route path="/Welcome" element={<Home />} />
      <Route
        path="*"
        element={
          <Authorized>
            <>
            <NavBar />
              <ApplicationViews />
              
            </>
          </Authorized>
        }
      />
    </Routes>
  );
};
