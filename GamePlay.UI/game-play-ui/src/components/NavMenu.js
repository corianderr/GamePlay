import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import { Link } from 'react-router-dom';
import useAuth from 'hooks/useAuth';
import useAxiosPrivate from 'hooks/useAxiosPrivate';
import { useEffect } from 'react';

export default function NavMenu() {
  const {auth, setAuth} = useAuth();
  const axiosPrivate = useAxiosPrivate();

  useEffect(() => {
    console.log(auth);
  }, [auth])

  const logout = () => {
    const clearCookies = async () => {
        const response = await axiosPrivate.post('/User/logout');
        setAuth({});
      }
      clearCookies();
  };

  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static">
        <Toolbar sx={{background: '#607d8b'}}>
          <IconButton
            size="large"
            edge="start"
            color="inherit"
            aria-label="menu"
            sx={{ mr: 2 }}
          >
          </IconButton>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            <Link to="/users">Users</Link>
          </Typography>
          {auth?.accessToken ? 
          <>
          <Link to={`/userDetails/${auth?.id}`} className='me-3'>Hello, {auth?.username}!</Link>
          <span onClick={logout} style={{cursor:'pointer'}}>Logout</span>
          </> :
          <Link to="/login">Login</Link>}
        </Toolbar>
      </AppBar>
    </Box>
  );
}