import React, { useState, useEffect } from "react";
import { makeStyles } from "@material-ui/core/styles";
import Card from "@material-ui/core/Card";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import CartItem from "./CartItem";
import { createAPIEndpoint, ENDPOINTS } from ".";
import MenuPizza from "./MenuPizza";
import { useNavigate } from "react-router-dom";

const useStyles = makeStyles({
  root: {
    minWidth: 475,
  },
  bullet: {
    display: "inline-block",
    margin: "0 2px",
    transform: "scale(0.8)",
  },
  title: {
    fontSize: 20,
  },
  pos: {
    marginBottom: 12,
  },
  dive: {
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
  },
});

export default function ShoppingCart(props) {
  const classes = useStyles();

  const [items, setItems] = useState([]);
  const [totalCost, setTotalCost] = useState(0);
  const navigate = useNavigate();
  
  useEffect (() => {
      var user = localStorage.getItem("PizzaAPIUsername");

      createAPIEndpoint(ENDPOINTS.cart)
          .getShoppingItems(user)
          .then(res => {
              setItems(res.data);
              var cost = 0;
              res.data.forEach(item => {
                  cost += item.price;
              })
              setTotalCost(cost.toFixed(2));
          })
          .catch(err => console.log(err));
  }, []);
  
  const gotoCreditCard = () => {
    navigate("/payment");
  }

  return (
    <div>
    <MenuPizza/>
      <Card className={(classes.root, classes.dive)} variant="outlined">
        <CardContent>
          <Typography className={classes.title} color="textSecondary" gutterBottom >
            Shopping Cart
          </Typography>
          <Typography variant="h5" component="h2">
            Your cart content:
          </Typography>
          <Typography className={classes.pos} color="textSecondary">
            Pizzas
          </Typography>
            {
              items.map((item,index) => {
                return <CartItem key = {index} name = {item.name} description = {item.description} price = {item.price}/>
              })
            }
          <br />
          <Typography className={classes.pos}>
            Total cost: {totalCost} lei
          </Typography>
        </CardContent>
        <CardActions>            
          <Button size="medium" variant="contained" color="primary"
            onClick={() =>{
              gotoCreditCard();
            }} > Pay </Button>
        </CardActions>
      </Card>
    </div>
  );
}
