import React, { useEffect, useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import Input from "@material-ui/core/Input";
import FormControl from "@material-ui/core/FormControl";
import InputLabel from "@material-ui/core/InputLabel";
import OutlinedInput from "@material-ui/core/OutlinedInput";
import clsx from "clsx";
import { createAPIEndpoint, ENDPOINTS } from ".";
import MenuPizza from "./MenuPizza";
import NotAllowedPage from "./NotAllowedPage"

const useStyles = makeStyles((theme) => ({
  root: {
    display: "wrap",
    flexDirection: "column",
  },
  textField: {
    width: "30ch",
  },
  button: {
    margin: theme.spacing(1),
  },
  margin: {
    marginLeft: theme.spacing(1),
    marginRight: theme.spacing(1),
    margin: theme.spacing(0.5),
  },
  withoutLabel: {
    marginTop: theme.spacing(3),
  },
}));

export default function AdminPage() {
    const classes = useStyles();

    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [price, setPrice] = useState(-1);
    const [idToDelete, setIdToDelete] = useState(-1);
    const [idToUpdate, setIdToUpdate] = useState(-1);
    const [nameToUpdate, setNameToUpdate] = useState("");
    const [priceToUpdate, setPriceToUpdate] = useState(-1);
    const [descriptionToUpdate, setDescriptionToUpdate] = useState("");

    const nameChangeHandler = (event) => {
        setName(event.target.value);
    };

    const priceChangeHandler = (event) => {
        setPrice(event.target.value);
    };

    const descriptionChangeHandler = (event) => {
        setDescription(event.target.value);
    };

    const idToDeleteChangeHandler = (event) => {
        setIdToDelete(event.target.value);
    };

    const idToUpdateChangeHandler = (event) => {
        setIdToUpdate(event.target.value);
    };

    const nameToUpdateChangeHandler = (event) => {
        setNameToUpdate(event.target.value);
    };

    const priceToUpdateChangeHandler = (event) => {
        setPriceToUpdate(event.target.value);
    };

    const descriptionToUpdateChangeHandler = (event) => {
        setDescriptionToUpdate(event.target.value);
    };

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [email, setEmail] = useState("");
    const [idUserToDelete, setIdUserToDelete] = useState("");
    const [usernameToUpdate, setUsernameToUpdate] = useState("");
    const [passwordToUpdate, setPasswordToUpdate] = useState("");
    const [emailToUpdate, setEmailToUpdate] = useState("");
    const [idUserToUpdate, setIdUserToUpdate] = useState("");

    const usernameChangeHandler = (event) => {
        setUsername(event.target.value);
    };

    const passwordChangeHandler = (event) => {
        setPassword(event.target.value);
    };

    const emailChangeHandler = (event) => {
        setEmail(event.target.value);
    };

    const idUserToDeleteChangeHandler = (event) => {
        setIdUserToDelete(event.target.value);
    };

    const usernameToUpdateChangeHandler = (event) => {
        setUsernameToUpdate(event.target.value);
    };

    const passwordToUpdateChangeHandler = (event) => {
        setPasswordToUpdate(event.target.value);
    };

    const emailToUpdateChangeHandler = (event) => {
        setEmailToUpdate(event.target.value);
    };

    const idUserToUpdateChangeHandler = (event) => {
        setIdUserToUpdate(event.target.value);
    };

    const [display, setDisplay] = useState(false);

    useEffect(() => {
        var role = localStorage.getItem("PizzaAPIUserRole");

        if (role === "ADMIN") {
            setDisplay(true);
        }
        else {
            setDisplay(false);
        }

    }, []);

    return (
        display ? <div className={classes.root}>
            <MenuPizza />
            <br />

            <form>
                <label style={{ fontSize: 50 }}>Pizza Area</label>
            </form>

            {/*-----------------------------------------------------------*/}
            <TextField
                id="name" label="Name" style={{ margin: 8 }} placeholder="Enter the name of the pizza." margin="normal"
                InputLabelProps={{ shrink: true }} onChange={nameChangeHandler} />
            <FormControl className={clsx(classes.margin, classes.withoutLabel, classes.textField)}>
                <Input id="Price" type="number" placeholder="Enter the pizza's price." onChange={priceChangeHandler} />
            </FormControl>
            <TextField id="description" label="Description" style={{ margin: 8 }} placeholder="Enter the pizza's description." margin="normal"
                InputLabelProps={{ shrink: true }} onChange={descriptionChangeHandler} />
            <Button id="addPizza" variant="contained" color="primary" size="medium" className={classes.button}
                onClick={() => {
                    createAPIEndpoint(ENDPOINTS.pizza)
                        .post({ name: name, price: price, description: description })
                        .then((res) => console.log(res))
                        .catch((error) => console.log(error));
                }}> Save </Button>
            {/*----------------------------------------------------------- ADD PIZZA*/}

            {/*-----------------------------------------------------------*/}
            <Button
                id="displayPizzas" variant="contained" color="primary" component="span" className={classes.button}
                onClick={() => {
                    createAPIEndpoint(ENDPOINTS.pizza)
                        .fetch()
                        .then((res) => console.log(res.data))
                        .catch((error) => console.log(error));
                }}> Display </Button>
            {/*----------------------------------------------------------- DISPLAY ALL PIZZAS*/}
            <br /><br />

            <TextField id="deletePizzabyId" label="Delete" style={{ margin: 8 }} placeholder="Enter the pizza's id." type="number"
                margin="normal" InputLabelProps={{ shrink: true }} onChange={idToDeleteChangeHandler} />
            <Button id="deletePizza" variant="contained" color="primary" component="span" className={classes.button}
                onClick={() => {
                    createAPIEndpoint(ENDPOINTS.pizza)
                        .delete(idToDelete)
                        .then((res) => console.log(res))
                        .catch((error) => console.log(error));
                }} > Delete </Button>
            {/*----------------------------------------------------------- DELETE PIZZA*/}
            <br /><br />

            {/*-----------------------------------------------------------*/}
            <TextField id="idToUpdatePizzaInfo" label="UpdateId" style={{ margin: 8 }} placeholder="Enter the pizza's id."
                type="number" margin="normal" InputLabelProps={{ shrink: true }} onChange={idToUpdateChangeHandler} />
            <TextField id="UpdateName" label="NameUpdate" style={{ margin: 8 }} placeholder="Enter the name of the pizza." margin="normal"
                InputLabelProps={{ shrink: true }} onChange={nameToUpdateChangeHandler} />
            <TextField id="UpdatePrice" label="PriceUpdate" type="number" style={{ margin: 8 }} placeholder="Enter the price of the pizza."
                margin="normal" InputLabelProps={{ shrink: true }} onChange={priceToUpdateChangeHandler} />
            <TextField id="DescriptionUpdate" label="DescriptionUpdate" style={{ margin: 8 }} placeholder="Enter the pizza's description."
                margin="normal" InputLabelProps={{ shrink: true }} onChange={descriptionToUpdateChangeHandler} />
            <Button id="updatePizza" variant="contained" color="primary" component="span" className={classes.button}
                onClick={() => {
                    createAPIEndpoint(ENDPOINTS.pizza)
                        .put(idToUpdate, {
                            id: idToUpdate,
                            name: nameToUpdate,
                            price: priceToUpdate,
                            description: descriptionToUpdate,
                        })
                        .then((res) => console.log(res))
                        .catch((error) => console.log(error));
                }} > Update </Button>
            <br /><br />
            {/*----------------------------------------------------------- UPDATE PIZZA*/}

            <form>
                <label style={{ fontSize: 50 }}>User Area</label>
            </form>

            {/*-----------------------------------------------------------*/}
            <form className={(classes.root, classes.margin)} noValidate autoComplete="off">
                <TextField id="username" variant="outlined" onChange={usernameChangeHandler} required label="Username" />
                <TextField id="email" type="email" variant="outlined" onChange={emailChangeHandler} required label="Email" />
                <FormControl className={(classes.margin, classes.textField)} variant="outlined" >
                    <InputLabel htmlFor="password">Password *</InputLabel>
                    <OutlinedInput id="password" type="text" onChange={passwordChangeHandler} required label="Password" labelWidth={70} />
                </FormControl>
                <Button variant="contained" color="primary" className={classes.button}
                    onClick={() => {
                        createAPIEndpoint(ENDPOINTS.user)
                            .post({
                                username: username,
                                password: password,
                                email: email,
                            })
                            .then((res) => console.log(res))
                            .catch((error) => console.log(error));
                    }} > Register </Button>
                {/*----------------------------------------------------------- ADD USER*/}

                {/*-----------------------------------------------------------*/}
                <Button
                    variant="contained"
                    color="primary"
                    className={classes.button}
                    onClick={() => {
                        createAPIEndpoint(ENDPOINTS.user)
                            .fetch()
                            .then((res) => console.log(res.data))
                            .catch((error) => console.log(error));
                    }} > Display </Button>
            </form> <br />
            {/*----------------------------------------------------------- DISPLAY ALL USERS*/}

            {/*-----------------------------------------------------------*/}
            <form className={(classes.root, classes.margin)} noValidate autoComplete="off" >
                <TextField id="idUserToDelete" label="IDToDelete" type="number" s variant="outlined" onChange={idUserToDeleteChangeHandler} />
                <Button variant="contained" color="primary" className={classes.button}
                    onClick={() => {
                        createAPIEndpoint(ENDPOINTS.user)
                            .delete(idUserToDelete)
                            .then((res) => console.log(res.data))
                            .catch((error) => console.log(error));
                    }}> Delete </Button>
                {/*----------------------------------------------------------- DELETE USER*/}
            </form> <br />

            {/*-----------------------------------------------------------*/}
            <form className={(classes.root, classes.margin)} noValidate autoComplete="off" >
                <TextField id="idUserToUpdate" label="IDToUpdate" type="number" variant="outlined" onChange={idUserToUpdateChangeHandler} />
                <TextField id="usernameToUpdate" label="UsernameToUpdate" variant="outlined" onChange={usernameToUpdateChangeHandler} />
                <TextField id="emailToUpdate" type="email" variant="outlined" label="EmailToUpdate" onChange={emailToUpdateChangeHandler} />
                <FormControl className={(classes.margin, classes.textField)} variant="outlined" >
                    <InputLabel htmlFor="passwordToUpdate">PasswordToUpdate</InputLabel>
                    <OutlinedInput id="passwordToUpdate" type="text" onChange={passwordToUpdateChangeHandler} labelWidth={70} />
                </FormControl>
                <Button id="updateUser" variant="contained" color="primary" component="span" className={classes.button}
                    onClick={() => {
                        createAPIEndpoint(ENDPOINTS.user)
                            .put(idUserToUpdate, {
                                userID: idUserToUpdate,
                                username: usernameToUpdate,
                                password: passwordToUpdate,
                                email: emailToUpdate,
                            })
                            .then((res) => console.log(res.data))
                            .catch((error) => console.log(error));
                    }} > Update </Button>
                <br />
                {/*----------------------------------------------------------- UPDATE USER*/}

                {/*-----------------------------------------------------------*/}
                <Button id="changeToUser" variant="contained" color="primary" component="span" className={classes.button}
                    onClick={() => {
                        createAPIEndpoint(ENDPOINTS.user)
                            .toUSER(idUserToUpdate)
                            .then()
                            .catch((error) => console.log(error));
                    }} > ToUSER </Button>
                {/*----------------------------------------------------------- PROMOTE TO USER*/}

                {/*-----------------------------------------------------------*/}
                <Button id="changeToAdmin" variant="contained" color="primary" component="span" className={classes.button}
                    onClick={() => {
                        createAPIEndpoint(ENDPOINTS.user)
                            .toADMIN(idUserToUpdate)
                            .then()
                            .catch((error) => console.log(error));
                    }} > ToADMIN </Button>
                {/*----------------------------------------------------------- PROMOTE TO ADMIN*/}
            </form>
        </div> : <div><NotAllowedPage /></div>
  );
}
