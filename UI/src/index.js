import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import MainRoute from "./MainRoute";
import axios from "axios";

export const BASE_URL = "https://localhost:7132/";

export const ENDPOINTS = {
  pizza: "Pizza",
  user: "User",
  cart: "ShoppingCart"
};

export const createAPIEndpoint = (endpoint) => {
  let url = BASE_URL + "api/" + endpoint + "/";

  return {
    fetch: () => axios.get(url + "GetAll"),
    fetchById: (id) => axios.get(url + "Get/" + id),
    post: (newRecord) => axios.post(url + "Add", newRecord),
    put: (id, updatedRecord) => axios.put(url + "Update/" + id, updatedRecord),
    delete: (id) => axios.delete(url + "Delete/" + id),
    login: (data) => axios.post(url + "Login", data),
    toUSER: (id) => axios.post(url + "ToUSER/" + id),
    toADMIN: (id) => axios.post(url + "ToADMIN/" + id),
    getLoggedUser: (cookie) => axios.post(url + "GetLoggedUser/" + cookie),
    getShoppingItems: (user) => axios.post(url + "GetItems/" + user),
    parseToken: (token) => axios.post(url + "parseToken/" + token),
    clearTheShoppingCart: (username) => axios.post(url + "clearShoppingCart/" + username),
    confirmAccount: (confirmationKey) => axios.post(url + "confirmYourAccount/" + confirmationKey)
  };
};

ReactDOM.render(
  <BrowserRouter>
    <MainRoute />
  </BrowserRouter>,
  document.getElementById("root")
);
