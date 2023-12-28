/*
https://www.youtube.com/watch?v=nI8PYZNFtac
React Login Authentication with JWT Access, Refresh Tokens, Cookies and Axios by Dave Gray
*/

/*
import { useRef, useState, useEffect } from 'react';
import useAuth from '../../components/Shared/hooks/useAuth';
import { Link, useNavigate, useLocation } from 'react-router-dom';


import axios from '../../api/axios';
const LOGIN_URL = '/login';

const LoginPage = () => {
  const { setAuth } = useAuth(); //OK

  const navigate = useNavigate();
  const location = useLocation();
  const from = location.state?.from?.pathname || "/";

  const userNameRef = useRef();
  const errRef = useRef();

  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const [errMsg, setErrMsg] = useState('');

  useEffect(() => {
    userNameRef.current.focus();
  }, [])

  useEffect(() => {
    setErrMsg('');
  }, [userName, password])

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
        const response = await axios.post(LOGIN_URL,
            JSON.stringify({ "userName": userName, "password": password }),
            {
                headers: { 'Content-Type': 'application/json' },
                withCredentials: true
            }
        );
        console.log(JSON.stringify(response?.data));
        //console.log(JSON.stringify(response));
        const accessToken = response?.data?.jwtToken;
        const { jwtToken, refreshToken } = response?.data;
        console.log('accessToken: ' + accessToken); //OK
        console.log('jwtToken: ' + jwtToken); //OK
        console.log('refreshToken: ' + refreshToken); //OK
        
        setAuth({ userName, password, accessToken }); //OK
        setUserName('');
        setPassword('');
        navigate(from, { replace: true });
    } catch (err) {
        if (!err?.response) {
            setErrMsg('No Server Response');
        } else if (err.response?.status === 400) {
            setErrMsg('Missing Username or Password');
        } else if (err.response?.status === 401) {
            setErrMsg('Unauthorized');
        } else {
            setErrMsg('Login Failed');
        }
        errRef.current.focus();
    }
  }

  return (
    <section>
      <div>LoginPage</div>
            <p ref={errRef} className={errMsg ? "errmsg" : "offscreen"} aria-live="assertive">{errMsg}</p>
            <h1>Sign In</h1>
            <form onSubmit={handleSubmit}>
                <label htmlFor="userName">Username:</label>
                <input
                    type="text"
                    id="userName"
                    ref={userNameRef}
                    autoComplete="off"
                    onChange={(e) => setUserName(e.target.value)}
                    value={userName}
                    required
                />

                <label htmlFor="password">Password:</label>
                <input
                    type="password"
                    id="password"
                    onChange={(e) => setPassword(e.target.value)}
                    value={password}
                    required
                />
                <button>Sign In</button>
            </form>
            <p>
                Need an Account?<br />
                <span className="line">
                    <Link to="/register">Sign Up</Link>
                </span>
            </p>
        </section>

  )
}

export default LoginPage;
*/