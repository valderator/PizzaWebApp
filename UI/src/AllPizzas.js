import React from "react";
import { useState, useEffect } from "react";
import { createAPIEndpoint, ENDPOINTS } from ".";
import { makeStyles } from '@material-ui/core/styles';
import Pizza from "./Pizza";

const useStyles = makeStyles({
  dive: {
      display: 'flex',
      flexDirection: 'row',
  }
});

export default function AllPizzas() {
  const classes = useStyles();
  const [pizzas, setPizzaas] = useState([]);
  const [userID, setUserID] = useState(-1);

  useEffect(() => {
    const token = localStorage.getItem("PizzaAPIUserToken");

      if (token !== "") {
          createAPIEndpoint(ENDPOINTS.user)
              .getLoggedUser(token)
              .then(res => {
                  if (res !== -1) {
                      setUserID(res);
                  }
              })
              .catch(err => console.log(err));
      }

    createAPIEndpoint(ENDPOINTS.pizza)
    .fetch()
    .then(res => {
      setPizzaas(res.data);
    })
    .catch(err => {
      console.log(err);
    })
  }, [])

  return (
    <div className={classes.dive}>
      {
      pizzas.map(pizza => {
          return <Pizza
                  key = {pizza.name}
                  name = {pizza.name}
                  description = {pizza.description}
                  price = {pizza.price}
                  id = {pizza.id} 
                  userID = {userID.data} />
        })
      }
    </div>
  );
}