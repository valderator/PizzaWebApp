import React, { useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import Cards from "react-credit-cards";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import "react-credit-cards/es/styles-compiled.css";
import MenuPizza from "./MenuPizza";
import { useNavigate } from "react-router-dom";
import { createAPIEndpoint, ENDPOINTS } from ".";

const useStyles = makeStyles((theme) => ({
  root: {
    "& > *": {
      margin: theme.spacing(2),
      display: "flex",
      justifyContent: "center",
      alignItems: "center",
    },
  },
  card: {
    margin: theme.spacing(3),
  },
  button2: {
    margin: theme.spacing(1),
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
  },
}));

export default function CreditCard() {
  const classes = useStyles();
  const navigate = useNavigate();

  const [name, setName] = useState("");
  const [number, setNumber] = useState("");
  const [expiry, setExpiry] = useState("");
  const [cvc, setCVC] = useState("");
  const [focused, setFocused] = useState("");

  const [errorName, setErrorName] = useState(false);
  const [errorNumber, setErrorNumber] = useState(false);
  const [errorExpiry, setErrorExpiry] = useState(false);
  const [errorCVC, setErrorCVC] = useState(false);

  const [nameHelper, setNameHelper] = useState("");
  const [numberHelper, setNumberHelper] = useState("");
  const [expiryHelper, setExpiryHelper] = useState("");
  const [cvcHelper, setCVCHelper] = useState("");

  const numberChangedHandler = (event) => {
    if (event.target.value.length > 16) {
      setErrorNumber(true);
      setNumberHelper("Maximum 16 characters.");
      event.preventDefault();
    } else {
      setErrorNumber(false);
      setNumberHelper("");
      setNumber(event.target.value);
    }
  };

  const nameChangedHandler = (event) => {
    if (event.target.value.length > 18) {
      setErrorName(true);
      setNameHelper("Maximum 18 characters.");
      event.preventDefault();
    }else {
      setErrorName(false);
      setNameHelper("");
      setName(event.target.value);
    }
  };

  const expiryChangedHandler = (event) => {
    if(event.target.value.length > 4){
      setErrorExpiry(true);
      setExpiryHelper("This date does not exists.");
      event.preventDefault();
    } else {
      setErrorExpiry(false);
      setExpiryHelper("");
      setExpiry(event.target.value);
    }
  };

  const cvcChangedHandler = (event) => {
    if (event.target.value.length > 3) {
      setErrorCVC(true);
      setCVCHelper("Maximum 3 characters.");
      event.preventDefault();
    } else {
      setErrorCVC(false);
      setCVCHelper("");
      setCVC(event.target.value);
    }
  };

  const changeFocusHandler = (event) => {
    setFocused(event.target.id);
  };

  return (
    <div>
      <MenuPizza />
      <div className={classes.card}>
        <Cards cvc={cvc} expiry={expiry} focused={focused} name={name} number={number} />
        <div className={classes.root}>
          <br></br>
          <TextField error={errorNumber} id="number" type="number" variant="outlined" placeholder="Card number"
                    helperText={numberHelper} onFocus={changeFocusHandler} onChange={numberChangedHandler} />
          <TextField error={errorName} id="name" variant="outlined" placeholder="Card-holder name"
                     helperText={nameHelper} onFocus={changeFocusHandler} onChange={nameChangedHandler} />
          <TextField error={errorExpiry} id="expiry" type="number" variant="outlined" placeholder="Expiry date"
                     helperText={expiryHelper} onFocus={changeFocusHandler} onChange={expiryChangedHandler} />
          <TextField error={errorCVC} id="cvc" type="number" variant="outlined" placeholder="CVC"
                     helperText={cvcHelper} onFocus={changeFocusHandler} onChange={cvcChangedHandler} />
          <div className={classes.button2}>
            <Button size="large" variant="contained" color="primary"
              onClick={() => {
                var username = localStorage.getItem("PizzaAPIUsername");
                createAPIEndpoint(ENDPOINTS.cart)
                  .clearTheShoppingCart(username)
                  .then((res) => {
                      alert("Your payment has been aproved, thank you for your purchase!");
                      navigate("/homepage");
                  })
                  .catch(err => console.log(err));
              }} > Pay </Button>
          </div>
        </div>
      </div>
    </div>
  );
}