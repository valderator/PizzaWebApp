import React from "react";
import MenuPizza from "./MenuPizza";
import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles((theme) => ({
    root: {
        margin: theme.spacing(30),
        fontSize: 40,
        textAlign:"center"
    },
}));

export default function NotAllowedPage() {

    const classes = useStyles();

    return (
        <div >
            <MenuPizza />
            <form className={classes.root}>
                You are not authorized to acces this page.
            </form> 
        </div>
    );
}