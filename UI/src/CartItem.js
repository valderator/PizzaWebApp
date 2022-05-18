import React from "react";
import Typography from "@material-ui/core/Typography";
import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles({
  root: {
    minWidth: 275,
  },
  bullet: {
    display: "inline-block",
    margin: "0 2px",
    transform: "scale(1.5)",
  },
});

export default function ListItem(props) {
  const classes = useStyles();
  const bull = <span className={classes.bullet}>•</span>;

  return (
    <div>
      <Typography variant="body2" component="p" style={{ marginLeft: ".5rem" }}>
        {bull} {props.name}
        <br />- {props.description}
        <br />
        Price: {props.price} lei
      </Typography>
      <br></br>
    </div>
  );
}
