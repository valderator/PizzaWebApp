import React from "react";
import MenuPizza from "./MenuPizza";
import AllPizzas from "./AllPizzas";

export default function Homepage() {
  return (
    <div>
      <MenuPizza /> <br/>
      <form>
        <label style={{ fontSize: 50 }}>"Our pizzas, the best pizzas."</label>
      </form> <br/>
      <AllPizzas />
    </div>
  );
}
