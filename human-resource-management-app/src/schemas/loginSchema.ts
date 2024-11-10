import * as yup from 'yup';

const loginSchema = yup.object().shape({
    username: yup.string().required('Vui lòng nhập username'),
    password: yup.string().required('Vui lòng nhập mật khẩu'),
   
});

export default loginSchema;
