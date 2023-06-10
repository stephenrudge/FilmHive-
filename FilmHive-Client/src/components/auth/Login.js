import { useState } from "react";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import "./Login.css";


export const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleLogin = (e) => {
    e.preventDefault();

    return fetch(`https://localhost:7211/api/Users/${email}`)
      .then((res) => {
        console.log(res)
        if
          (res.status === 404) {
          console.log("user not found")
        }
        else {
          return res.json()
        }
      })
      .then((foundUser) => {
        console.log(foundUser)
        if (!foundUser || foundUser.password !== password) {
          window.alert("Invalid login");
        }
        else {

          console.log(foundUser)
          localStorage.setItem(
            "capstone_user",
            foundUser.id
          );

          navigate("/Discover");
        }
      }
      );
  };

  return (
    <>
    <main className="container--login">
    <section>

          <form className="form--login" >

            <div class="login-box">
             
              <p>Login</p>
              <form>
                <div class="user-box">
                  <input
                    required
                    type="text"
                    value={email}
                    onChange={(evt) => setEmail(evt.target.value)} />
                  <label>Email</label>
                </div>
                <div class="user-box">
                <input
                    required
                    type="password"
                    value={password}
                    onChange={(evt) => setPassword(evt.target.value)} />
                  <label>Password</label>
                </div>
                <a href="#" onClick={handleLogin}>
                  <span></span>
                  <span></span>
                  <span></span>
                  <span></span>
                  Submit
                </a>
              </form>
              <p>Not a member yet?</p>  <Link to="/register">Sign Up!</Link>

            </div>
          </form>

        </section>
        </main>
      </>






    //    <main className="container--login">
    //      <section>
    //        <form className="form--login" onSubmit={handleLogin}>
    //          <h1>FilmHive</h1>
    //          <h2>Please sign in</h2>
    //          <fieldset>
    //            <label htmlFor="inputEmail"> Email address </label>
    //            <input
    //              type="email"
    //              value={email}
    //              onChange={(evt) => setEmail(evt.target.value)}
    //              className="form-control"
    //              placeholder="Email address"
    //              required
    //              autoFocus
    //            />
    //          </fieldset>

    //          <fieldset>
    //            <label htmlFor="Password"> Password </label>
    //            <input
    //              type="Password"
    //              value={password}
    //              onChange={(evt) => setPassword(evt.target.value)}
    //              className="form-control"
    //              placeholder="Password"
    //              required

    //            />
    //          </fieldset>
    //          <fieldset>
    //            <button type="submit">Sign in</button>
    //          </fieldset>
    //        </form>
    //      </section>
    //      <section className="link--register">
    //        <Link to="/register">Not a member yet?</Link>
    //      </section>
    // </main> 
  )
  
};
