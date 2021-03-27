package com.example.springbootwebapi.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

/**
 * ClassName: HelloController <br/>
 * Description: <br/>
 * date: 2021/3/27 16:30<br/>
 *
 * @author Adminstor<br />
 */
@Controller
public class HelloController {
    @RequestMapping(path = "/")
    @ResponseBody
    public String GetStringMethod(){
        return "hello spring boot!";
    }
}
