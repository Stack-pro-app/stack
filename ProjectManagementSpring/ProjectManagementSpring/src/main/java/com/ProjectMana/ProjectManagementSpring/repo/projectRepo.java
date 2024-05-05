package com.ProjectMana.ProjectManagementSpring.repo;

import com.ProjectMana.ProjectManagementSpring.enteties.Task;
import com.ProjectMana.ProjectManagementSpring.enteties.WorkSpace;
import com.ProjectMana.ProjectManagementSpring.enteties.project;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface projectRepo extends JpaRepository<project,Integer> {
    project findByprojectName (String name);
    List<project> findAllByWorkSpaceIn(List<WorkSpace> wrk ) ;

}
