import { React, useState } from "react";
import { createAPIEndpoint, ENDPOINTS } from ".";
import { Box, Button, TextField } from "@material-ui/core";
import { useNavigate } from "react-router-dom";

export default function ConfirmAccount() {

    const [confirmationKey, setConfirmationKey] = useState("");
    const navigate = useNavigate();

    const confirmationChangeHandler = (event) => {
        setConfirmationKey(event.target.value);
    }

    const sendConfirmationKey = () => {
        createAPIEndpoint(ENDPOINTS.user)
            .confirmAccount(confirmationKey)
            .then((res) => {
                if (res.data === true) {
                    alert("Account confirmed succesfully, you will be redirect to the homepage.");
                    navigate("/homepage");
                } else {
                    alert("Something wrong happened, try again.");
                }
            })
            .catch((err) => console.log(err));
    }

    return (
        <Box sx={{ margin:20 }}>
            <label style={{ fontSize: 30 }}>Enter your confirmation key: </label>
            <Box sx={{ display: "flex", p:2 }}>
                <TextField id="confirmationKey" variant="outlined" onChange={confirmationChangeHandler} label="Confirmation Key"/>
            </Box>
            <Box sx={{ display: "flex", pl:2 }}>
                <Button variant="contained" color="primary" onClick={sendConfirmationKey}> Confirm </Button>
            </Box>
        </Box>
    );
}
