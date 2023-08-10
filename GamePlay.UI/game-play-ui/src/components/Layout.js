import { Outlet } from "react-router-dom";
import NavMenu from "./NavMenu";
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

const Layout = () => {
  return (
    <>
      <NavMenu />
      <main className="App container">
          <Outlet />
          <ToastContainer/>
      </main>
    </>
  )
}

export default Layout