import React, { useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import clsx from "clsx";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import FormControl from "@material-ui/core/FormControl";
import InputLabel from "@material-ui/core/InputLabel";
import OutlinedInput from "@material-ui/core/OutlinedInput";
import { createAPIEndpoint, ENDPOINTS } from ".";
import MenuPizza from "./MenuPizza";
import { useNavigate } from "react-router-dom";

const useStyles = makeStyles((theme) => ({
  root: {
    "& > *": {
      margin: theme.spacing(2),
      width: "25ch",
      display: "flex",
      flexDirection: "row",
      justifyContent: "center",
      alignItems: "center",
    },
  },
  root2: {
    display: "flex",
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center",
  },
  button: {
    margin: theme.spacing(2),
  },
  margin: {
    margin: theme.spacing(2),
  },
  reset: {},
}));

export default function Login() {
  const classes = useStyles();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const usernameChangeHandler = (event) => {
    setUsername(event.target.value);
  };

  const passwordChangeHandler = (event) => {
    setPassword(event.target.value);
  };

  const gotoHomepage = () => {
    navigate("/homepage");
  };

  return (
    <div>
      <MenuPizza/>
      <div className={classes.root2}>
        <form className={classes.root} noValidate autoComplete="on">
          <div>
            <label style={{ fontSize: 50 }}>Login</label>
          </div>
          <TextField id="username" variant="outlined" required onChange={usernameChangeHandler} label="Username"/>
          <FormControl className={clsx(classes.margin, classes.textField)} variant="outlined">
            <InputLabel htmlFor="password">Password *</InputLabel>
            <OutlinedInput id="password" type="password" required onChange={passwordChangeHandler} label="Password" labelWidth={70} />
          </FormControl>
          <Button variant="contained" color="primary"
            onClick={() => {
              createAPIEndpoint(ENDPOINTS.user)
                .login({ username: username, password: password })
                  .then((res) => {
                      var token = res.data;
                      if (token.length < 30) {
                          alert(token);
                      } else {
                          localStorage.setItem("PizzaAPIUserToken", token);

                          createAPIEndpoint(ENDPOINTS.user)
                              .parseToken(token)
                                  .then((res) => {
                                      var info = res.data;
                                      localStorage.setItem("PizzaAPIUsername", info[0]);
                                      localStorage.setItem("PizzaAPIUserRole", info[1]);
                                  })
                                  .catch((err) => console.log(err));

                          alert("Logged in successfully!");
                          gotoHomepage();
                      }
                })
                .catch((err) => console.log(err));
            }}> Login </Button>
        </form>
      </div>
    </div>
  );
}
