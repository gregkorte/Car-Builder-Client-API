import {
  getWheels,
  getInteriors,
  getTechnologies,
  getPaints,
  getOrders,
  completeOrder,
} from "./database.js";

const paints = await getPaints();
const interiors = await getInteriors();
const techs = await getTechnologies();
const wheels = await getWheels();

document.addEventListener("click", (event) => {
  const { name, id } = event.target;
  if (name === "complete") {
    completeOrder(id);
  }
});

export const Orders = async () => {
  const orders = await getOrders();

  return `${orders
    .map((order) => {
      const paint = paints.find((p) => p.id === order.paintColorId);
      const technology = techs.find((t) => t.id === order.technologyId);
      const interior = interiors.find((i) => i.id === order.interiorId);
      const wheel = wheels.find((w) => w.id === order.wheelId);

      return `<section class="order">
                ${paint.color} car with
                ${wheel.style} wheels,
                ${interior.material} interior,
                and the ${technology.package}
                for a total cost of
                ${order.totalCost.toLocaleString("en-US", {
                  style: "currency",
                  currency: "USD",
                })}
            <input type="button" name="complete" id="${
              order.id
            }" value="Complete">
            </section>`;
    })
    .join("")}`;
};
