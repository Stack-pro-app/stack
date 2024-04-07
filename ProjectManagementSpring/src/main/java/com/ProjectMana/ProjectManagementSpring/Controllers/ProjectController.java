package com.ProjectMana.ProjectManagementSpring.Controllers;

import com.ProjectMana.ProjectManagementSpring.enteties.project;
import com.ProjectMana.ProjectManagementSpring.repo.projectRepo;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
public class ProjectController {
private projectRepo  prjRepo;

    public ProjectController(projectRepo prjRepo) {
        this.prjRepo = prjRepo;
    }
    @PostMapping("/project")
    public project create(@RequestBody project prj ){
        return this.prjRepo.save(prj);

    }
    @GetMapping("/project/{id}")
    public project get(@PathVariable int id ){
        return this.prjRepo.findById(id).orElse(null);
    }
    @GetMapping("/project/exists/{name}")
    public boolean get(@PathVariable String name ){
        project p =  this.prjRepo.findByprojectName(name);
        if(p!=null){
            return true ;
        }
        else {
            return false ;
        }

    }
    @GetMapping("/project")
    public List<project> findall(){
       return this.prjRepo.findAll();
    }
    @DeleteMapping("/project/{id}")
    public ResponseEntity<Void> delete(@PathVariable int id) {
        this.prjRepo.deleteById(id);
        return ResponseEntity.noContent().build();
    }


}
