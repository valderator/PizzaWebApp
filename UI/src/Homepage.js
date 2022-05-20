import React from "react";
import MenuPizza from "./MenuPizza";
import AllPizzas from "./AllPizzas";

export default function Homepage() {
  return (
      <div>
          <MenuPizza /> <br />
          <label style={{ fontSize: 50 }}>"Our pizzas, the best pizzas."</label><br/><br/>
          <AllPizzas />
      </div>
  );
}
