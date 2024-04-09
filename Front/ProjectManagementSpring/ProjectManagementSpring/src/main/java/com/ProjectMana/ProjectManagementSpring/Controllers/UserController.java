package com.ProjectMana.ProjectManagementSpring.Controllers;

import com.ProjectMana.ProjectManagementSpring.DTO.userDTO;
import com.ProjectMana.ProjectManagementSpring.enteties.UserT;
import com.ProjectMana.ProjectManagementSpring.repo.UserRepo;
import com.ProjectMana.ProjectManagementSpring.services.userService;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
public class UserController {
    private UserRepo userRespo ;
    userService userService;

    public UserController(UserRepo userRespo, com.ProjectMana.ProjectManagementSpring.services.userService userService) {
        this.userRespo = userRespo;
        this.userService = userService;
    }

    @PostMapping("/user")
    public UserT create(@RequestBody UserT userT){
       return this.userRespo.save(userT);
    }

    @GetMapping ("/user")
    public List<userDTO> getAll(){
        return  this.userService.getAll();

    }

    @GetMapping ("/userAndTasks")
    public List<UserT> getAllTasks(){
        return  this.userRespo.findAll();

    }
}
