import React, { useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import clsx from "clsx";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import FormControl from "@material-ui/core/FormControl";
import InputLabel from "@material-ui/core/InputLabel";
import OutlinedInput from "@material-ui/core/OutlinedInput";
import { createAPIEndpoint, ENDPOINTS } from ".";
import { useNavigate } from "react-router-dom";
import MenuPizza from "./MenuPizza";

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

export default function Register() {
  const classes = useStyles();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [email, setEmail] = useState("");
  const [showPassword, setPasswordVisib] = useState(false);
  const navigate = useNavigate();

  const usernameChangeHandler = (event) => {
    setUsername(event.target.value);
  };

  const passwordChangeHandler = (event) => {
    setPassword(event.target.value);
  };

  const emailChangeHandler = (event) => {
    setEmail(event.target.value);
  };

  return (
    <div>
     <MenuPizza/>
      <div className={classes.root2}>
        <form className={classes.root} noValidate autoComplete="off">
          <div>
            <label style={{ fontSize: 50 }}>Register</label>
          </div>
          <TextField id="username" variant="outlined" onChange={usernameChangeHandler} required label="Username" />
          <TextField id="email" type="email" variant="outlined" onChange={emailChangeHandler} required label="Email" />
          <FormControl className={clsx(classes.margin, classes.textField)} variant="outlined">
            <InputLabel htmlFor="password">Password *</InputLabel>
            <OutlinedInput id="password" type={showPassword ? "text" : "password"} onChange={passwordChangeHandler}
                           required label="Password" labelWidth={70} />
          </FormControl>
          <Button variant="contained" color="primary"
            onClick={() => {
              createAPIEndpoint(ENDPOINTS.user)
                .post({
                  username: username,
                  password: password,
                  role: "USER",
                  email: email,
                })
                .then((res) => {
                  console.log(res);
                  if (res.data != null) {
                    alert("Account created succesfully!");
                    navigate("/homepage");
                  }
                })
                .catch((error) => {
                  console.log(error);
                  alert("Something wrong happed, try again.");
                });
            }}
          > Register </Button>
        </form>
      </div>
    </div>
  );
}
