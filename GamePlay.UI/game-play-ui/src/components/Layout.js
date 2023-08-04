import { Outlet } from "react-router-dom";
import NavMenu from "./NavMenu";

const Layout = () => {
  return (
    <>
      <NavMenu />
      <main className="App">
          <Outlet />
      </main>
    </>
  )
}

export default Layout