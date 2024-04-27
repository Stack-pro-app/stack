package com.ProjectMana.ProjectManagementSpring.Controllers;

import com.ProjectMana.ProjectManagementSpring.DTO.projectDTO;
import com.ProjectMana.ProjectManagementSpring.enteties.Task;
import com.ProjectMana.ProjectManagementSpring.enteties.project;
import com.ProjectMana.ProjectManagementSpring.repo.TaskRepo;
import com.ProjectMana.ProjectManagementSpring.repo.projectRepo;
import com.ProjectMana.ProjectManagementSpring.services.projectService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
public class ProjectController {
private projectRepo  prjRepo;
private TaskRepo TaskRepo ;
private projectService projectService ;

    public ProjectController(projectRepo prjRepo, com.ProjectMana.ProjectManagementSpring.repo.TaskRepo taskRepo, com.ProjectMana.ProjectManagementSpring.services.projectService projectService) {
        this.prjRepo = prjRepo;
        TaskRepo = taskRepo;
        this.projectService = projectService;
    }

    @PostMapping(value = "/project")
    public projectDTO create(@RequestBody projectDTO prj ){
        return this.projectService.post(prj);

    }
    @GetMapping("/project/{id}")
    public projectDTO get(@PathVariable int id ){
        return this.projectService.getById(id);
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
    public List<projectDTO> findall(){
      return this.projectService.getAll();

    }
    @DeleteMapping("/project/{id}")
    public ResponseEntity<Void> delete(@PathVariable int id) {



        this.prjRepo.deleteById(id);
        return ResponseEntity.noContent().build();
    }
    @GetMapping("/project/IdByName/{name}")
  public Integer getIdByName(@PathVariable String name){
      return this.projectService.getIdByName(name);

    }
  @GetMapping("/project/getPro/{name}")
  public ResponseEntity<projectDTO> getByName(@PathVariable String name) {
    project p = this.prjRepo.findByprojectName(name);
    if (p != null) {
      projectDTO projectDTO = this.projectService.getById(p.getId());
      return ResponseEntity.ok(projectDTO);
    } else {
      return ResponseEntity.notFound().build();
    }
  }

}
