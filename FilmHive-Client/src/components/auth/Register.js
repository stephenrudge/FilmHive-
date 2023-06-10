import { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Login.css";

export const Register = (props) => {
  const [user, setUser] = useState({
    email: "",
    Name: "",
    password: ""
  });
  let navigate = useNavigate();

  const registerNewUser = () => {
    console.log(user)

    return fetch("https:localhost:7211/api/Users", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(user),
    })
      .then((res) => res.json())
      .then((createdUser) => {
        if (createdUser.hasOwnProperty("id")) {
          localStorage.setItem(
            "capstone_user",
            JSON.stringify({
              id: createdUser.id,
            })
          );

          navigate("/Discover");
        }
      });
  };

  const handleRegister = (e) => {
    e.preventDefault();
    return fetch(`https://localhost:7211/api/Users?email=${user.email}`)
      .then((res) => res.json())
      .then((response) => {
        console.log(response)
        var users = response.filter((Obj) => Obj.email == user.email)
        if (users.length > 0) {
          // Duplicate email. No good.
          window.alert("Account with that email address already exists");
        } else {
          // Good email, create user.
          registerNewUser();
        }
      });
  };

  const updateUser = (evt) => {
    const copy = { ...user };
    copy[evt.target.id] = evt.target.value;
    setUser(copy);
  };

  return (
    <>
      <main className="container--register">

        <section>

          <form className="form--register" onSubmit={handleRegister}>

            <div className="login-box">
              <p>Register</p>
              <form>
                <div className="user-box">
                  <input
                    required
                    type="text"
                    autoFocus
                    onChange={updateUser} />
                  <label>Name</label>
                </div>
                <div class="user-box">
                  <input
                    type="email"
                    id="email"
                    className="form-control"
                    required
                    onChange={updateUser} />

                  <label>Email</label>
                </div>
                <div class="user-box">
                  <input
                    type="password"
                    id="password"
                    className="form-control"
                    required
                    onChange={updateUser} />
                  <label>Password</label>
                </div>
                <a href="#" onClick={handleRegister}>
                  <span></span>
                  <span></span>
                  <span></span>
                  <span></span>
                   Register 
                </a>
              </form>
              </div>
          </form>

        </section>
      </main>
    </>
)
}
     {/* <main style={{ textAlign: "center" }}>
       <form className="form--login" onSubmit={handleRegister}>
         <h1 className="h3 mb-3 font-weight-normal">Please Register</h1>
         <fieldset>
           <label htmlFor="fullName"> Full Name </label>
           <input
             onChange={updateUser}
             type="text"
             id="Name"
             className="form-control"
             placeholder="Enter your name"
             required
             autoFocus
           />
         </fieldset>
         <fieldset>
           <label htmlFor="email"> Email address </label>
           <input
             onChange={updateUser}
             type="email"
             id="email"
             className="form-control"
             placeholder="Email address"
             required
           />
         </fieldset>
         <fieldset>
           <label htmlFor="password"> Password </label>
           <input
             onChange={updateUser}
             type="password"
             id="password"
             className="form-control"
             placeholder="Password"
             required
           />
         </fieldset>
         <fieldset>
           <button type="submit"> Register </button>
         </fieldset>
       </form>
     </main> */}

