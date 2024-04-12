package com.ProjectMana.ProjectManagementSpring.Controllers;

import com.ProjectMana.ProjectManagementSpring.DTO.GanttDTO;
import com.ProjectMana.ProjectManagementSpring.DTO.taskDTO;
import com.ProjectMana.ProjectManagementSpring.DTO.taskDTO1;
import com.ProjectMana.ProjectManagementSpring.enteties.Task;
import com.ProjectMana.ProjectManagementSpring.enteties.project;
import com.ProjectMana.ProjectManagementSpring.repo.TaskRepo;
import com.ProjectMana.ProjectManagementSpring.services.taskService;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
public class TaskController {
    private TaskRepo TaskRepo ;

  private taskService taskService;

    public TaskController(com.ProjectMana.ProjectManagementSpring.repo.TaskRepo taskRepo, com.ProjectMana.ProjectManagementSpring.services.taskService taskService) {
        TaskRepo = taskRepo;
        this.taskService = taskService;
    }

    @PostMapping("/task")
    public taskDTO create(@RequestBody taskDTO Task){
      return this.taskService.create(Task);

    }

    @GetMapping("/task")
    public List<taskDTO1> getAll(){
        return this.taskService.getAll();
    }

    @DeleteMapping("/task/{id}")
    public void delete(@PathVariable Integer id ){
        this.TaskRepo.deleteById(id);

    }

    @GetMapping("/task/{no}")
    public taskDTO1 getByNo(@PathVariable Integer no){
        return this.taskService.getByNo(no);
    }
  @GetMapping("/task/Gantt")
  public List<project> getGantData(){
      return this.taskService.pp();
  }





}
