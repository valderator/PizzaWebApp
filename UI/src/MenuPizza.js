import React, { useState } from "react";
import Button from "@material-ui/core/Button";
import Menu from "@material-ui/core/Menu";
import MenuItem from "@material-ui/core/MenuItem";
import { useNavigate } from "react-router-dom";

export default function MenuPizza() {
  const [anchorEl, setAnchorEl] = useState(null);
  const navigate = useNavigate();
  const [state, setState]= useState(false);


  const handleClick = (event) => setAnchorEl(event.currentTarget);

  const handleClose = () => setAnchorEl(null);

  const goToLogin = () => navigate("/login");

  const goToRegister = () => navigate("/register");

  const goToShoppingCart = () => navigate("/shoppingCart");

  const gotoHomepage = () => navigate("/homepage");

  const goToAdminPage = () => navigate("/");

  const goToMyAccountSettings = () => navigate("/myAccount");

  const signOut = () => {
      localStorage.setItem("PizzaAPIUserToken", "");
      localStorage.setItem("PizzaAPIUsername", "");
      localStorage.setItem("PizzaAPIUserRole", "");
      alert("You have been signed out.");
      gotoHomepage();
    }

  const alert2 = () => {
      alert("You must be logged in to see your shopping cart.");
  }

  return (
    <div>
      <Button aria-controls="simple-menu" aria-haspopup="true" variant="contained" color="primary" size="large" onClick={handleClick}>Menu</Button>
          <Menu id="simple-menu" keepMounted anchorEl={anchorEl} open={Boolean(anchorEl)} onClose={handleClose}>
              {localStorage.getItem("PizzaAPIUsername") !== "" ?
                  <div>
                      <MenuItem onClick={goToMyAccountSettings}> My account</MenuItem>
                      <MenuItem onClick={signOut}>Signout</MenuItem>
                  </div>
                  :
                  <div>
                      <MenuItem onClick={goToLogin}>Login</MenuItem>
                      <MenuItem onClick={goToRegister}>Register</MenuItem>
                  </div>
              }
        <MenuItem onClick={gotoHomepage}>Homepage</MenuItem>
              {localStorage.getItem("PizzaAPIUsername") !== "" ?
                  <MenuItem onClick={goToShoppingCart} >Shopping Cart</MenuItem>
                  :
                  <MenuItem onClick={alert2} >Shopping Cart</MenuItem>}
        {localStorage.getItem("PizzaAPIUserRole") === "ADMIN" ? <MenuItem onClick={goToAdminPage}> Admin page </MenuItem> : null}
      </Menu>
    </div>
  );
}
