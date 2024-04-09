package com.ProjectMana.ProjectManagementSpring.services;

import com.ProjectMana.ProjectManagementSpring.DTO.taskDTO;
import com.ProjectMana.ProjectManagementSpring.DTO.taskDTO1;
import com.ProjectMana.ProjectManagementSpring.enteties.Task;
import com.ProjectMana.ProjectManagementSpring.enteties.UserT;
import com.ProjectMana.ProjectManagementSpring.enteties.project;
import com.ProjectMana.ProjectManagementSpring.repo.TaskRepo;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
@Service
public class taskService {
    private TaskRepo taskRepo ;

    public taskService(TaskRepo taskRepo) {
        this.taskRepo = taskRepo;
    }
    public taskDTO create(taskDTO t ){
        Task ts = new Task(null,t.title,t.description,t.end,0,new project(t.projectId),new UserT(t.userId));
        Task ts1 = this.taskRepo.save(ts);
        t.no=ts1.getNo();
        return t ;

    }
    public List<taskDTO1> getAll(){
        List<taskDTO1> l = new ArrayList<>();
        List<Task> l1 = this.taskRepo.findAll();
        if(!l1.isEmpty()){
            for(Task t : l1){
                l.add(new taskDTO1(t.getNo(),t.getTitle(),t.getDescription(),t.getProject().getProjectName(),t.getUser().userName,t.getEnd(),t.getStatus()));
            }

        }
        return l ;

    }
    public taskDTO1 getByNo(Integer no ){
        Task t  = this.taskRepo.findById(no).get();

        return new taskDTO1(t.getNo(),t.getTitle(),t.getDescription(),t.getProject().getProjectName(),t.getUser().userName,t.getEnd(),t.getStatus());

    }
}
