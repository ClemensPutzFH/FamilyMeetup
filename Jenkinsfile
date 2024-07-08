pipeline {
    agent{
        label 'jenkins-agent'
    }

    environment {
        APP_NAME = 'family-meetup'
        RELEASE = '1.0.0'
        DOCKER_USER = 'maxmustermann1736'
        DOCKER_PASS = 'dockerhub'
        IMAGE_NAME = "${DOCKER_USER}/${APP_NAME}"
        IMAGE_TAG = "${RELEASE}-${BUILD_NUMBER}"

        SONARQUBE_URL = 'https://192.168.57.22'
        SONARQUBE_TOKEN = credentials('jenkins-sonarqube-token')
    }
    
    stages {
        stage("Cleanup Workspace") {
            steps {
                cleanWs()
            }
        }
        stage('Checkout from SCM') {
            steps {
                git branch: 'pipeline', url: 'https://github.com/ClemensPutzFH/FamilyMeetup.git'
            }
        }

        stage('Restore') {
            steps {
                script {
                    sh 'dotnet restore'
                }
            }
        }
        stage('Build') {
            steps {
                sh 'dotnet build'
            }
        }

        stage('Publish Tests') {
                sh 'dotnet publish FamilyMeetup.Tests/FamilyMeetup.Tests.csproj --configuration Release --output ./publish'
            }

        stage('Test') {
            steps {
                    sh 'dotnet test'
            }
        }
        stage('Install SonarScanner') {
            steps {
                script {
                    // Check if SonarScanner is installed, if not, install it
                    def toolInstalled = sh(script: 'dotnet tool list -g | grep dotnet-sonarscanner', returnStatus: true)
                    if (toolInstalled != 0) {
                        sh 'dotnet tool install --global dotnet-sonarscanner'
                    } else {
                        echo 'SonarScanner already installed'
                    }
                    // Ensure the .NET tools path is added to the PATH environment variable
                    env.PATH = "${env.HOME}/.dotnet/tools:${env.PATH}"
                }
            }
        }
        stage('Build + SonarQube analysis') {
            steps {
                script {
                    withSonarQubeEnv(credentialsId: 'jenkins-sonarqube-token') {
                        sh 'dotnet sonarscanner begin /k:"family-meetup" /d:sonar.login=$SONAR_AUTH_TOKEN'
                        sh 'dotnet build'
                        sh 'dotnet sonarscanner end /d:sonar.login=$SONAR_AUTH_TOKEN'
                    }
                }
            }
        }
        

        stage("Build & Push Docker Image") {
            steps {
                script {
                    docker.withRegistry('', "${DOCKER_PASS}") {
                        def dockerImage = docker.build("${IMAGE_NAME}")

                        dockerImage.push("${IMAGE_TAG}")
                        dockerImage.push('latest')
                    }
                }
            }
        }   
    }
    post {
        always {
            cleanWs()
        }
    }
}
