import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Card from "@material-ui/core/Card";
import CardActionArea from "@material-ui/core/CardActionArea";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import CardMedia from "@material-ui/core/CardMedia";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import img from "./pizza.jpg";
import { createAPIEndpoint, ENDPOINTS } from ".";

const useStyles = makeStyles((theme) => ({
  root: {
    maxWidth: 345,
    margin: theme.spacing(1),
  },
  media: {
    height: 320,
    width: 500,
  },
}));

export default function Pizza(props) {
  const classes = useStyles();

  return (
    <div>
      <Card className={classes.root}>
        <CardActionArea>
          <CardMedia className={classes.media} image={img} title={props.name} />
          <CardContent>
            <Typography gutterBottom variant="h5" component="h2"> {props.name} </Typography>
            <Typography variant="body2" color="textSecondary" component="p"> {props.description} <br/> The price is: {props.price} lei
            </Typography>
          </CardContent>
        </CardActionArea>
        <CardActions>
          <Button size="small" color="primary"
            onClick={() => {
              if(localStorage.getItem("PizzaAPIUsername") === ""){
                  alert("You must login first.");
              } else {
                  createAPIEndpoint(ENDPOINTS.cart)
                      .post({ userID: props.userID, pizzaID: props.id })
                      .then(res => {
                          alert("Pizza added to the shopping cart!");
                      })
                      .catch(err => console.log(err));
              }
              
            }}> Add to cart </Button>
        </CardActions>
      </Card>
    </div>
  );
}
