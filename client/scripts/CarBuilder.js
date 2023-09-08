import { addCustomOrder } from "./database.js";
import { Interiors } from "./Interiors.js";
import { Orders } from "./Orders.js";
import { Paints } from "./Paints.js";
import { Technologies } from "./Technologies.js";
import { Wheels } from "./Wheels.js";
import { getCurrentState } from "./database.js";

document.addEventListener("click", async (event) => {
  if (event.target.id === "orderButton") {
    await addCustomOrder();
  }
});

export const CarBuilder = async () => {
  return `
        <h1>Cars 'R Us: Personal Car Builder</h1>

        <article class="choices">
            <section class="choices__metals options">
                ${Paints()}
            </section>
            <section class="choices__sizes options">
                ${Interiors()}
            </section>
            <section class="choices__styles options">
                ${Wheels()}
            </section>
            <section class="choices__styles options">
                ${Technologies()}
            </section>
        </article>

        <article>
            <button id="orderButton">Place Car Order</button>
        </article>

        <article class="customOrders">
            <h2>Custom Car Orders</h2>
            ${await Orders()}
        </article>
    `;
};
