import React, { useState } from "react";
import Button from "@material-ui/core/Button";
import Menu from "@material-ui/core/Menu";
import MenuItem from "@material-ui/core/MenuItem";
import { useNavigate } from "react-router-dom";

export default function MenuPizza() {
  const [anchorEl, setAnchorEl] = useState(null);
  const navigate = useNavigate();

  const handleClick = (event) => setAnchorEl(event.currentTarget);

  const handleClose = () => setAnchorEl(null);

  const goToLogin = () => navigate("/login");

  const goToRegister = () => navigate("/register");

  const goToShoppingCart = () => navigate("/shoppingCart");

  const gotoHomepage = () => navigate("/homepage");

  const goToAdminPage = () => navigate("/");

  return (
    <div>
      <Button aria-controls="simple-menu" aria-haspopup="true" variant="contained" color="primary" size="large" onClick={handleClick}>Menu</Button>
      <Menu id="simple-menu" keepMounted anchorEl={anchorEl} open={Boolean(anchorEl)} onClose={handleClose}>
        <MenuItem onClick={goToLogin}>Login</MenuItem>
        <MenuItem onClick={goToRegister}>Register</MenuItem>
        <MenuItem onClick={gotoHomepage}>Homepage</MenuItem>
        <MenuItem onClick={goToShoppingCart}>Shopping Cart</MenuItem>
        <MenuItem onClick={goToAdminPage}> Admin page </MenuItem>
      </Menu>
    </div>
  );
}
