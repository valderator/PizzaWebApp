import React from "react";
import { Routes, Route } from "react-router-dom";
import AdminPage from "./AdminPage";
import Register from "./Register";
import Homepage from "./Homepage";
import ShoppingCart from "./ShoppingCart";
import CreditCard from "./CreditCard";
import Login from "./Login";
import ConfirmAccount from "./ConfirmAccount";

export default function MainRoute() {

  return (
    <Routes>
      <Route path="/" element={<AdminPage />} />
      <Route path="/register" element={<Register />} />
      <Route path="/homepage" element={<Homepage />} />
      <Route path="/shoppingCart" element={<ShoppingCart />} />
      <Route path="/login" element={<Login />} />
      <Route path="/payment" element={<CreditCard />} />
      <Route path="/confirmYourAccount" element={<ConfirmAccount />} />
    </Routes>
  );
}
