package com.ProjectMana.ProjectManagementSpring.repo;

import com.ProjectMana.ProjectManagementSpring.DTO.projectDTO;
import com.ProjectMana.ProjectManagementSpring.enteties.Task;
import com.ProjectMana.ProjectManagementSpring.enteties.WorkSpace;
import com.ProjectMana.ProjectManagementSpring.enteties.project;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Component;

import java.util.List;

@Component
public interface TaskRepo extends JpaRepository<Task,Integer> {
    Task findByTitle (String title);
  List<Task> findAllByProjectIn(List<project> projects ) ;
}
